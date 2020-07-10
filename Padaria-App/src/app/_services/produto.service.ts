import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Produto } from '../_models/Produto';

@Injectable({
  providedIn: 'root'/*faz com que o serviço seja injetado em toda a aplicação*/
})
export class ProdutoService {
  baseURL = 'http://localhost:5000/api/produto';
  tokenHeader: HttpHeaders;

  constructor(private http: HttpClient) {
    this.tokenHeader = new HttpHeaders({ 'Authorization': `Bearer ${localStorage.getItem('token')}` });
  }

  getAllProduto(): Observable<Produto[]> {
    return this.http.get<Produto[]>(this.baseURL);
  }

  getProdutoById(id: string): Observable<Produto[]> {
    return this.http.get<Produto[]>(`${this.baseURL}/${id}`, { headers: this.tokenHeader });
  }

  postUpload(file: File, name: string) {
    const fileToUpload = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, name);

    return this.http.post(`${this.baseURL}/upload`, formData, {
      headers: this.tokenHeader,
      observe: 'events',
      reportProgress: true
    });
  }

  postProduto(produto: Produto) {
    return this.http.post(this.baseURL, produto, { headers: this.tokenHeader });
  }

  putProduto(produto: Produto) {
    return this.http.put(`${this.baseURL}/${produto.id}`, produto, { headers: this.tokenHeader });
  }

  deleteProduto(id: string) {
    return this.http.delete(`${this.baseURL}/${id}`, { headers: this.tokenHeader });
  }
}
