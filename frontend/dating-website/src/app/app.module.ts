import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/loginPage/login/login.component';
import { RegisterComponent } from './components/RegisterPage/register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './components/home/home/home.component';
import { NavComponent } from './components/navbar/nav/nav.component';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized/unauthorized.component';
import { ProfileComponent } from './components/profile/profile/profile.component';
import { PhoneMaskDirective } from './derectives/phone-mask.directive';
import { CriteriaComponent } from './components/search-criteria/criteria/criteria.component';
import { SearchResultComponent } from './components/search-result/search-result/search-result.component';
import { SearchResultItemComponent } from './components/search-result-item/search-result-item/search-result-item.component';
import { SearchResultPageComponent } from './components/search-result-page/search-result-page/search-result-page.component';
import { SearchResultPaginationComponent } from './components/search-result-pagination/search-result-pagination/search-result-pagination.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    NavComponent,
    UnauthorizedComponent,
    ProfileComponent,
    PhoneMaskDirective,
    CriteriaComponent,
    SearchResultComponent,
    SearchResultItemComponent,
    SearchResultPageComponent,
    SearchResultPaginationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
