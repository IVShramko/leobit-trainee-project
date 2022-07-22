import { TokenInterceptor } from './interceptors/token.interceptor';
import { AuthGuard } from './guards/AuthGuard';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/loginPage/login/login.component';
import { RegisterComponent } from './components/RegisterPage/register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HomeComponent } from './components/home/home/home.component';
import { NavComponent } from './components/navbar/nav/nav.component';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized/unauthorized.component';
import { ProfileComponent } from './components/profile/profile/profile.component';
import { PhoneMaskDirective } from './directives/phone-mask.directive';
import { CriteriaComponent } from './components/search-criteria/criteria/criteria.component';
import { SearchResultComponent } from './components/search-result/search-result/search-result.component';
import { SearchResultItemComponent } from './components/search-result-item/search-result-item/search-result-item.component';
import { SearchResultPageComponent } from './components/search-result-page/search-result-page/search-result-page.component';
import { SearchResultPaginationComponent } from './components/search-result-pagination/search-result-pagination/search-result-pagination.component';
import { MainProfileComponent } from './components/main-profile/main-profile/main-profile.component';
import { SearchSettingsComponent } from './components/search-settings/search-settings/search-settings.component';
import { FileUploadDirective } from './directives/file-upload.directive';
import { AccountComponent } from './components/account/account/account.component';
import { AlbumComponent } from './components/album/album/album.component';
import { AlbumsComponent } from './components/albums/albums/albums.component';
import { PhotoComponent } from './components/photo/photo/photo.component';
import { ViewPhotoNameDirective } from './directives/view-photo-name.directive';
import { PhotoViewerComponent } from './components/photo-viewer/photo-viewer/photo-viewer.component';

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
    SearchResultPaginationComponent,
    MainProfileComponent,
    SearchSettingsComponent,
    FileUploadDirective,
    AccountComponent,
    AlbumComponent,
    AlbumsComponent,
    PhotoComponent,
    ViewPhotoNameDirective,
    PhotoViewerComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
