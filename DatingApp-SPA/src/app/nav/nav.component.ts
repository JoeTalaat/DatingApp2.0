import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/Auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model : any={};

  constructor(private authService:AuthService) { }

  ngOnInit() {
  }

  login(){
    this.authService.login(this.model).subscribe(next=>{
      console.log('Loggin in successfully')
    },error =>{
      console.log('failed to login')
    });
  }

  loggedIn(){
    const token = localStorage.getItem('token');
    return !!token; //if something in the token i till return ture if not will retun false
  }

  logout(){
    localStorage.removeItem('token');
    console.log('Logged out');
  }
}
