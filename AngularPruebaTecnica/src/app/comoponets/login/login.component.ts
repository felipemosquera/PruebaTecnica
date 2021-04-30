import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  constructor(
    private apiService:ApiService,
    private router:Router
  ) {
    this.loginForm = new FormGroup(
      {
        'email':new FormControl('',{validators:Validators.compose([Validators.required,Validators.email])}),
        'password':new FormControl('',{validators:Validators.compose([Validators.required])})
      }
    )
   }

  ngOnInit(): void {
  }

  submit(){
    this.apiService.login(this.loginForm.value).subscribe(
      (value) => {
        if(value.Users){
          localStorage.setItem("login","true");
          this.router.navigate(['person'])
        }else{
          console.log("maaaaal")
        }
      }
    )
  }


}
