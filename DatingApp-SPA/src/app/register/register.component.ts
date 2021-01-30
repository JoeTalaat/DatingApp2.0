import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AleartifyService } from '../_services/aleartify.service';
import { AuthService } from '../_services/Auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Input() valuesFromHome :any;
  @Output() cancelRegister = new EventEmitter();
  model:any ={};

  constructor(private authService:AuthService , private aleartify : AleartifyService) { }

  ngOnInit() {
  }

register(){
  this.authService.register(this.model).subscribe(
    ()=>{this.aleartify.success('Registeration successful')},error=> {
      this.aleartify.error(error)
    }
  );
  console.log(this.model);
}
cancel(){
  this.cancelRegister.emit(false); //can be an object or simple boolean as we use now
  console.log('canceled');
}


}
