import { AuthService } from 'src/app/services/auth-service/auth.service';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate
{
    constructor(private authService: AuthService, private router: Router) {}

    canActivate(): boolean {
        const status = this.authService.Authenticate();
        status ? status : this.router.navigate(['unauthorized']);
        return status;
    }

}