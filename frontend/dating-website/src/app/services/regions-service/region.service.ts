import { Injectable } from '@angular/core';
import { Regions } from 'src/app/enums/regions';

@Injectable({
  providedIn: 'root'
})
export class RegionService {

  constructor() {
    this.regions = Object.keys(Regions).filter(f => isNaN(Number(f)));
  }

  private regions: string[] = [];

  GetAllRegions() {
    return this.regions;
  }
}
