import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Person } from 'src/app/Models/Person.model';
import { ApiService } from 'src/app/services/api.service';
import { User } from '../../Models/user.model';

@Component({
  selector: 'app-persons',
  templateUrl: './persons.component.html',
  styleUrls: ['./persons.component.scss']
})
export class PersonsComponent implements OnInit {
  users :Observable<any> = new Observable<any>();
  resgisterForm: FormGroup;
  UserForm:FormGroup;
  constructor(
    private apiService:ApiService,
    private router:Router,
  ) {
    this.resgisterForm = new FormGroup(
      {
        'Name':new FormControl('',{validators:Validators.compose([Validators.required,Validators.email])}),
        'LastName':new FormControl('',{validators:Validators.compose([Validators.required])}),
        'Document':new FormControl('',{validators:Validators.compose([Validators.required])}),
        'DocumentType':new FormControl('',{validators:Validators.compose([Validators.required])}),
        'Email':new FormControl('',{validators:Validators.compose([Validators.required])}),
      }),

      this.UserForm = new FormGroup(
        {
          'password':new FormControl('',{validators:Validators.compose([Validators.required])})
        }  ,

    )
   }

  ngOnInit(): void {
     let login = localStorage.getItem("login");
     if(login != "true") this.router.navigate(['/'])
    this.users = this.apiService.getPersons();
  }

  submit(){
    console.log(this.resgisterForm.value)
      this.apiService.addNewPerson(this.resgisterForm.value).subscribe(
        (person) => {
          if (person) {
            this.addPerson(person);
            this.users = this.apiService.getPersons()
          }
        }
      )
    
  }

  addPerson(person:Person){
    console.log(person)
    this.apiService.AddNewUser({
      "IdPerson": person.Id,
      "Password": this.UserForm.value.password,
    }).subscribe(
      (user) => {
        console.log(user)
      }
    )
  }

}
