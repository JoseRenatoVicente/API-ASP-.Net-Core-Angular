import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProdutosComponent } from './produtos/produtos.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PedidosComponent } from './pedidos/pedidos.component';
import { FuncionariosComponent } from './funcionarios/funcionarios.component';
import { ClientesComponent } from './clientes/clientes.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { AuthGuard } from './auth/auth.guard';

const routes: Routes = [
  { path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent }
    ]
  },

  { path: '', component: DashboardComponent },
  { path: 'produtos', component: ProdutosComponent },
  { path: 'pedidos', component: PedidosComponent, canActivate: [AuthGuard]},
  { path: 'funcionarios', component: FuncionariosComponent, canActivate: [AuthGuard]},
  { path: 'clientes', component: ClientesComponent, canActivate: [AuthGuard]},
  /*{ path: '', redirectTo: '', pathMatch: 'full' }, * Ocorre quando nada é digitado */
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

/* ForRoot(toda aplicação) */

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
