import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';
import { FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  titulo = 'Login';
  model: any = {};

  constructor(private authService: AuthService,
              public fb: FormBuilder,
              private toastr: ToastrService,
              public router: Router) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigate(['']);
    }
  }

  login() {
    this.authService.login(this.model)
    .subscribe(
      () => {
        this.router.navigate(['']);
      },
      error => {
        this.toastr.error('Login ou senha invalidos');
      }
    )
  }

}
