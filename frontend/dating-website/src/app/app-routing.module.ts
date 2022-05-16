import { UnauthorizedComponent } from './components/unauthorized/unauthorized/unauthorized.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home/home.component';
import { LoginComponent } from './components/loginPage/login/login.component';
import { RegisterComponent } from './components/RegisterPage/register/register.component';

const routes: Routes = [
  {
    path : 'home', 
    component : HomeComponent,
  },
  {
    path : 'login', 
    component : LoginComponent,
  },
  {
    path : 'register', 
    component : RegisterComponent
  },
  {
    path : 'unauthorized', 
    component : UnauthorizedComponent,
  },
  {
    path : '', 
    component : HomeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
