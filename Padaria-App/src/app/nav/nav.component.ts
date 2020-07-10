import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(private authService: AuthService,
              public router: Router,
              private toastr: ToastrService) { }

  ngOnInit() {
  }

  entrar() {
    this.router.navigate(['/user/login']);
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.toastr.show('logout');
    this.router.navigate(['/user/login']);
  }

  userName() {
    return localStorage.getItem('username');
  }

}
