import { Injectable } from '@angular/core';
import { Observable, retry } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserAuthorization } from '../types/userAuthorization';
import { User } from '../types/user';
import { Nullable } from '../types/nullable';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  constructor(private httpClient: HttpClient) {}

  authorize(access_token: string): Observable<User> {
    const headers: HttpHeaders = new HttpHeaders();
    headers.set('Authorization', `Basic ${access_token}`);

    return this.httpClient.post<User>(
      'http://localhost:3000/api/auth',
      {},
      { headers }
    ).pipe(
      retry(2),
    );
  }

  isAuthorized(): Nullable<string> {
    const accessToken = this.getCookie('access_token');

    if (!accessToken) {
      return null;
    }

    return accessToken;
  }

  getCookie(cookieToFind: string): Nullable<string> {
    const cookies: Array<string> = decodeURIComponent(document.cookie).split('; ');

    for (let cookie of cookies) {
        const userCookie = cookie.split('=');

        if (userCookie[0] === cookieToFind) {
          return userCookie[1];
        }
    }

    return null;
  }
}
