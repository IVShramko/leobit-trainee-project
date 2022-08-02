import { SEARCH_PATH } from '../../paths';
import { SearchResultDTO } from 'src/app/models/search-result/SearchResultDTO';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { ProfileCriteria } from 'src/app/models/search-criteria/profileCriteria';
import { CriteriaDTO } from 'src/app/models/search-criteria/CriteriaDTO';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private server: HttpClient) { }

  private readonly path: string = SEARCH_PATH;

  ProfileCriteria = new Subject<ProfileCriteria>();

  Search(criteria: CriteriaDTO): Observable<SearchResultDTO> {
    return this.server.post<SearchResultDTO>(this.path, criteria);
  }
}
