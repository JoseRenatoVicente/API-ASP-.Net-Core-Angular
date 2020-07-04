import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProdutosComponent } from './produtos/produtos.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PedidosComponent } from './pedidos/pedidos.component';
import { FuncionariosComponent } from './funcionarios/funcionarios.component';
import { ClientesComponent } from './clientes/clientes.component';


const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'produtos', component: ProdutosComponent },
  { path: 'pedidos', component: PedidosComponent },
  { path: 'funcionarios', component: FuncionariosComponent },
  { path: 'clientes', component: ClientesComponent },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' }, /** Ocorre quando nada é digitado */
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
];

/* ForRoot(toda aplicação) */

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
