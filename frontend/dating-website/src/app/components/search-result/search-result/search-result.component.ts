import { CriteriaDTO } from '../../../models/search-criteria/CriteriaDTO';
import { SearchService } from './../../../services/search-service/search.service';
import { Component, OnInit } from '@angular/core';
import { SearchResultDTO } from 'src/app/models/search-result/SearchResultDTO';
import { Observable, Subject } from 'rxjs';
import { ProfileCriteria } from 'src/app/models/search-criteria/profileCriteria';
import { ProfileSearchResult } from 'src/app/models/search-result/profileSearchResult';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.css']
})
export class SearchResultComponent implements OnInit {

  constructor(private searchService: SearchService) { }

  searchResults$ = new Observable<SearchResultDTO>();
  ResultsTotal$ = new Subject<number>();
  Profiles$ = new Subject<ProfileSearchResult[]>();
  PageIndex: number = 1;
  PageSize: number = 2;
  SearchFilter: number = 0;
  private _profileCriteria: ProfileCriteria;

  ngOnInit(): void {
    this.searchService.ProfileCriteria.subscribe((profileCriteria) => {
      this._profileCriteria = profileCriteria;
      this.LoadResultPage();
    })
  }

  private LoadResultPage() {
    const fullCriteria = this.GetFullCriteria(this._profileCriteria);
    this.GetPageData(fullCriteria);
  }

  OnPageIndexChange(index: number) {
    this.PageIndex = index;
    this.LoadResultPage();
  }

  OnPageSizeChange(index: number) {
    this.PageSize = index;
    this.LoadResultPage();
  }

  OnSearchFilterChange(filter: number) {
    this.SearchFilter = filter;
    this.LoadResultPage();
  }

  private GetPageData(fullCriteria: CriteriaDTO) {
    this.searchService.Search(fullCriteria).subscribe(
      (result) => {
        this.ResultsTotal$.next(result.resultsTotal);
        this.Profiles$.next(result.profiles);
      });
  }

  private GetFullCriteria(profileCriteria: ProfileCriteria) {
    const fullCriteria: CriteriaDTO = {
      pageIndex: this.PageIndex,
      pageSize: this.PageSize,
      Filter: this.SearchFilter,
      profile: profileCriteria
    }

    return fullCriteria;
  }

}
