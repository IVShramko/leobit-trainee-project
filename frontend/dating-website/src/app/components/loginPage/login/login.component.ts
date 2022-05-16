import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/authService/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm : FormGroup;
  isError: boolean;

  constructor(private formBuiler : FormBuilder,
    private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.formBuiler.group({
      login : [
        null,
        Validators.required
      ],
      password : [
        null,
        Validators.required
      ]
    });
  }

  get login()
  {
    return this.loginForm.controls.login;
  }

  get password()
  {
    return this.loginForm.controls.password;
  }

  OnlogIn (userName: string, password: string)
  {
    this.authService.LogIn(userName, password);

    this.authService.isLoggedIn.subscribe(
      (result) => {
        if (result) {
          this.router.navigate(['/home']);
        }
        this.isError = !result;
      }
    )
  }

}
