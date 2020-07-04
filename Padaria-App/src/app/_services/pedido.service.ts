import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pedido } from '../_models/Pedido';

@Injectable({
  providedIn: 'root'/*faz com que o serviço seja injetado em toda a aplicação*/
})
export class PedidoService {
  baseURL = 'http://localhost:5000/api/pedido';

  constructor(private http: HttpClient) { }

  getAllPedido(): Observable<Pedido[]> {
    return this.http.get<Pedido[]>(this.baseURL);
  }

  getPedidoById(id: number): Observable<Pedido[]> {
    return this.http.get<Pedido[]>('${this.baseURL}/${id}');
  }


}
