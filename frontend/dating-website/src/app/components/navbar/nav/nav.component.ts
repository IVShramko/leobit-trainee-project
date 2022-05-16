import { AuthService } from 'src/app/services/authService/auth.service';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public authService: AuthService, private router: Router) {}

  ngOnInit(): void {
  }

  @Input() isLoggedIn: boolean;
  @Input() userName: string;

  OnLogOut()
  {
    this.authService.LogOut();
    this.router.navigate(['/']);
  }

}
