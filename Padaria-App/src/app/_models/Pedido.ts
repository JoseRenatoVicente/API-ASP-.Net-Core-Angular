import { Venda } from './Venda';
import { Produto } from './Produto';

export interface Pedido {

    id: string ;
    valorTotal: number ;
    dataInicio?: Date ;
    dataFinal?: Date ;
    qrcode: string ;
    clienteId: string ;
    vendas: Produto[];

}
