import { SafeResourceUrl } from '@angular/platform-browser';
import { tap } from 'rxjs';
import { PhotoMainDTO } from 'src/app/models/photoMainDTO';
import { PhotoService } from './../../../services/photo-service/photo.service';
import { ImageUtility } from './../../../utilities/image-utility';
import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { ProfileSearchResult } from 'src/app/models/search-result/profileSearchResult';

@Component({
  selector: 'app-search-result-item',
  templateUrl: './search-result-item.component.html',
  styleUrls: ['./search-result-item.component.css']
})
export class SearchResultItemComponent implements OnInit, OnChanges {

  constructor(private imageUtility: ImageUtility,
    private photoService: PhotoService) {
  }

  @Input() Profile: ProfileSearchResult;
  avatar: SafeResourceUrl | undefined;

  ngOnChanges(): void {
    this.photoService.GetSearchResultAvatar(this.Profile.id, this.Profile.avatar)
      .pipe(
        tap((photo: PhotoMainDTO) => {
          if (photo !== null) {
            this.avatar = this.imageUtility
              .ConvertToSafeResourceUrl(photo.data, photo.name);
          }
        })
      ).subscribe();

  }

  ngOnInit(): void {
  }

}
