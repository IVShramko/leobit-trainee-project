import { UserService } from './services/user/user.service';
import { AuthService } from 'src/app/services/authService/auth.service';
import { Component, Output } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'dating-website';

  @Output() isLoggedIn: boolean;
  @Output() userName: string;

  constructor(private authService: AuthService, private userService: UserService) {

    this.authService.ValidateToken();
    this.authService.isLoggedIn.subscribe(
      (result) => {
        this.isLoggedIn = result
        if (result) {
          this.userName = this.userService.user.UserName;
        }
      }
    );
  }
}
