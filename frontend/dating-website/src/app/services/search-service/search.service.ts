import { SEARCH_PATH } from './../../Paths';
import { SearchResult } from 'src/app/models/SearchResult';
import { HttpClient } from '@angular/common/http';
import { Criteria, ProfileCriteria } from './../../models/Criteria';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private server: HttpClient) { }

  private readonly path: string = SEARCH_PATH;

  ProfileCriteria = new Subject<ProfileCriteria>();

  Search(criteria: Criteria): Observable<SearchResult>
  {
    return this.server.post<SearchResult>(this.path + 'criteria', criteria);
  }
}
