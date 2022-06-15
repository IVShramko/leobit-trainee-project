import { PAGE_SIZE } from './../../../Constants';
import { ProfileCriteria } from './../../../models/Criteria';
import { SearchService } from './../../../services/search-service/search.service';
import { Component, OnInit, Input } from '@angular/core';
import { SearchResult, SearchResultUserProfile } from 'src/app/models/SearchResult';
import { Observable, Subject } from 'rxjs';
import { Criteria } from 'src/app/models/Criteria';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.css']
})
export class SearchResultComponent implements OnInit {

  constructor(private searchService: SearchService) { }

  searchResults$ = new Observable<SearchResult>();
  ResultsTotal$ = new Subject<number>();
  Profiles$ = new Subject<SearchResultUserProfile[]>();
  PageIndex: number = 1;
  PageSize: number = 2;
  SearchFilter: string = 'Age';
  private _profileCriteria: ProfileCriteria;

  ngOnInit(): void 
  {
    this.searchService.ProfileCriteria.subscribe((profileCriteria) => 
    {
      this._profileCriteria = profileCriteria;
      this.LoadResultPage();
    })
  }

  private LoadResultPage()
  {
    const fullCriteria = this.GetFullCriteria(this._profileCriteria);
    this.GetPageData(fullCriteria);
  }

  OnPageIndexChange(index: number)
  {
    this.PageIndex = index;
    this.LoadResultPage();
  }

  OnPageSizeChange(index: number)
  {
    this.PageSize = index;
    this.LoadResultPage();
  }

  OnSearchFilterChange(filter: string)
  {
    this.SearchFilter = filter;
    this.LoadResultPage();
  }
  
  private GetPageData(fullCriteria: Criteria)
  {
    this.searchService.Search(fullCriteria).subscribe(
      (result) => {
        this.ResultsTotal$.next(result.resultsTotal);
        this.Profiles$.next(result.profiles);
        });
  }

  private GetFullCriteria(profileCriteria: ProfileCriteria)
  {
    const fullCriteria: Criteria = {
      pageIndex: this.PageIndex,
      pageSize: this.PageSize,
      searchFilter: this.SearchFilter,
      profile: profileCriteria
    }

    return fullCriteria;
  }

}
