import { SearchService } from './../../../services/search-service/search.service';
import { Criteria, ProfileCriteria } from './../../../models/Criteria';
import { RegionService } from './../../../services/regions-service/region.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-criteria',
  templateUrl: './criteria.component.html',
  styleUrls: ['./criteria.component.css']
})
export class CriteriaComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
    private regionService: RegionService, private searchService: SearchService) 
  {
    this.regions = this.regionService.GetAllRegions();
  }

  criteriaForm: FormGroup;
  regions: string[] = [];
  private readonly MAX_AGE = 60;
  private readonly MIN_AGE = 18;

  ngOnInit(): void {
    this.criteriaForm = this.formBuilder.group({
      gender: [
        null,
        Validators.required
      ],
      minAge: [
        this.MIN_AGE
      ],
      maxAge: [
        this.MAX_AGE
      ], 
      region: [
        null
      ],
      town: [
        null
      ]
    })
  }
  get gender()
  {
    return this.criteriaForm.controls.gender;
  }

  get minAge()
  {
    return this.criteriaForm.controls.minAge;
  }

  get maxAge()
  {
    return this.criteriaForm.controls.maxAge;
  }

  get region()
  {
    return this.criteriaForm.controls.region;
  }

  get town()
  {
    return this.criteriaForm.controls.town;
  }

  private GetProfileCriteria()
  {
    const criteria: ProfileCriteria = {
      Gender: this.gender?.value,
      MinAge: this.minAge?.value,
      MaxAge: this.maxAge?.value,
      Region: this.region?.value,
      Town: this.town?.value
    }

    return criteria;
  }

  RunSearch()
  {
    this.searchService.ProfileCriteria.next(this.GetProfileCriteria());
  }
  
}
