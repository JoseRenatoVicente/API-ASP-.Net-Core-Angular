import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Produto } from '../_models/Produto';

@Injectable({
  providedIn: 'root'/*faz com que o serviço seja injetado em toda a aplicação*/
})
export class ProdutoService {
  baseURL = 'http://localhost:5000/api/produto';

  constructor(private http: HttpClient) { }

  getAllProduto(): Observable<Produto[]> {
    return this.http.get<Produto[]>(this.baseURL);
  }

  getProdutoById(id: string): Observable<Produto[]> {
    return this.http.get<Produto[]>('${this.baseURL}/${id}');
  }

  postUpload(file: File, name: string) {
    const fileToUpload = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, name);

    return this.http.post(`${this.baseURL}/upload`, formData);
  }

  postProduto(produto: Produto) {
    return this.http.post(this.baseURL, produto);
  }

  putProduto(produto: Produto) {
    return this.http.put(`${this.baseURL}/${produto.id}`, produto);
  }

  deleteProduto(id: string) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
