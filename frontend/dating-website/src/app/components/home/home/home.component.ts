import { AuthService } from 'src/app/services/authService/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) 
  {
    
  }

  isLoggedIn: boolean;

  ngOnInit(): void {
    this.authService.isLoggedIn.subscribe(
      (result) => this.isLoggedIn = result
    )
  }
}
