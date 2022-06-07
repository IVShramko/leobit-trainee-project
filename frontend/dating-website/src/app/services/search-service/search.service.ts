import { HttpClient } from '@angular/common/http';
import { Criteria } from './../../models/Criteria';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private server: HttpClient) { }

  private readonly searchPath: string = "https://localhost:44362/api/search";

  CriteriaRequest(critria: Criteria)
  {
    return this.server.post<any>(this.searchPath + 'criteria', critria);
  }
}
