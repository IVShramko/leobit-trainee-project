import { ChatComponent } from './components/chat/chat/chat.component';
import { PhotoEditorComponent } from './components/photo-editor/photo-editor/photo-editor.component';
import { AlbumsComponent } from './components/albums/albums/albums.component';
import { AccountComponent } from './components/account/account/account.component';
import { AuthGuard } from './guards/authGuard';
import { ProfileComponent } from './components/profile/profile/profile.component';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized/unauthorized.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home/home.component';
import { LoginComponent } from './components/loginPage/login/login.component';
import { RegisterComponent } from './components/RegisterPage/register/register.component';
import { AlbumComponent } from './components/album/album/album.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent,
  },
  {
    path: 'editor/:id',
    component: PhotoEditorComponent,
  },
  {
    path: 'chat',
    component: ChatComponent,
  },
  {
    path: 'account',
    component: AccountComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'profile',
        component: ProfileComponent,
      },
      {
        path: 'albums',
        component: AlbumsComponent
      },
      {
        path: 'albums/:id',
        component: AlbumComponent
      },
      {
        path: '**',
        redirectTo: 'profile'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
