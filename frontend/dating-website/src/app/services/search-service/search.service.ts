import { SearchResult } from 'src/app/models/SearchResult';
import { HttpClient } from '@angular/common/http';
import { Criteria } from './../../models/Criteria';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private server: HttpClient) { }

  private readonly searchPath: string = "https://localhost:44362/api/search";

  criteria = new Subject<Criteria>();

  Search(criteria: Criteria): Observable<SearchResult[]>
  {
    console.log('3');
    return this.server.post<SearchResult[]>(this.searchPath + '/criteria', criteria);
  }
}
