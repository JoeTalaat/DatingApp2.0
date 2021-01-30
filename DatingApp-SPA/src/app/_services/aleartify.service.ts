import { Injectable } from '@angular/core';
import * as aleartify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class AleartifyService {

constructor() { }

 confirm(message : string,okCallback : () => any)
 {
   aleartify.confirm(message,(e:any)=> {
    if(e)
    {
      okCallback();
    }
    else {}

   });
 }

  success(message:string){
    aleartify.success(message);
  }


  error(message:string){
    aleartify.error(message);
  }

  
  warning(message:string){
    aleartify.warning(message);
  }

  
  message(message:string){
    aleartify.message(message);
  }


}
