/*Modulos */

import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

/*Serviços */
import { ProdutoService } from './_services/produto.service';

/*Componentes */
import { AppComponent } from './app.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserComponent } from './user/user.component';
import { NavComponent } from './nav/nav.component';
import { ProdutosComponent } from './produtos/produtos.component';
import { PedidosComponent } from './pedidos/pedidos.component';
import { FuncionariosComponent } from './funcionarios/funcionarios.component';
import { ClientesComponent } from './clientes/clientes.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TituloComponent } from './_shared/titulo/titulo.component';

import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';

registerLocaleData(localePt);

/*Pipes */

@NgModule({
   declarations: [
      AppComponent,
      LoginComponent,
      RegistrationComponent,
      UserComponent,
      NavComponent,
      ProdutosComponent,
      PedidosComponent,
      FuncionariosComponent,
      ClientesComponent,
      DashboardComponent,
      TituloComponent
   ],
   imports: [
      BrowserModule,
      BsDropdownModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      ToastrModule.forRoot({
         positionClass: 'toast-bottom-right',
         preventDuplicates: true,
       }),
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      BrowserAnimationsModule,
      ReactiveFormsModule
   ],
   providers: [
      {provide: LOCALE_ID, useValue: 'pt-Br'},
      ProdutoService,
      ReactiveFormsModule
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }