import { AuthService } from 'src/app/services/auth-service/auth.service';
import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) { }

    canActivate(): boolean {
        const status = this.authService.Authenticate();
        status ? status : this.router.navigate(['unauthorized']);
        return status;
    }

}