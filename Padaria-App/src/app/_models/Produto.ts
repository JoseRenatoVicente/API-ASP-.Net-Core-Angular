import { Venda } from './Venda';
import { Pedido } from './Pedido';

export interface Produto {

     id: string;
     nome: string;
     preco: number;
     descricao: string;
     status: string;
     imagemUrl: string;
     vendas: Pedido[];
}
