import { Inject, Injectable, InjectionToken } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class BrowserStorageService {
  constructor() {}

  get(key: string) {
    return localStorage.getItem(key);
  }

  set(key: string, value: string) {
    localStorage.setItem(key, value);
  }

  remove(key: string) {
    localStorage.removeItem(key);
  }

  clear() {
    localStorage.clear();
  }
}