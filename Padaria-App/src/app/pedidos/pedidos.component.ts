

import { Component, OnInit } from '@angular/core';
import { PedidoService } from '../_services/pedido.service';
import { Pedido } from '../_models/Pedido';

@Component({
  selector: 'app-pedidos',
  templateUrl: './pedidos.component.html',
  styleUrls: ['./pedidos.component.css']
})
export class PedidosComponent implements OnInit {


  private pedidoService: PedidoService;
  constructor() { }

  _filtroLista: string;
  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.pedidosFiltrados = this.filtroLista ? this.filtrarPedidos(this.filtroLista) : this.pedidos;
  }

  pedidosFiltrados: Pedido [];
  pedidos: Pedido [];
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;



  ngOnInit() {
    this.getPedidos();
  }

  filtrarPedidos(filtrarPor: string): Pedido[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.pedidos.filter(
      pedido => pedido.vendas[0].nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  getPedidos() {
     this.pedidoService.getAllPedido().subscribe(
       (_pedidos: Pedido[]) => {
         this.pedidos = _pedidos;
         console.log(_pedidos); },
       error => {
         console.log(error);
       });
  }

}


