import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class TokenManager
{
    GetToken(name: string) : string | null
    {
      return localStorage.getItem(name);
    }
  
    SetToken(name: string, token: string) : void
    {
      localStorage.setItem(name, token);
    }
  
    DeleteToken(name: string)
    {
      localStorage.removeItem(name);
    }
}