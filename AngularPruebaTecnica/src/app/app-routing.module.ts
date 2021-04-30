import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './comoponets/login/login.component';
import { PersonsComponent } from './comoponets/persons/persons.component';

const routes: Routes = [
  {path :'login',component:LoginComponent},
  {path :'person',component:PersonsComponent},
  {path :'**',component:LoginComponent},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
