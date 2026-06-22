import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UsuarioListar } from '../Models/Usuarios';
import { Response } from '../Models/Response';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  apiUrl = environment.urlApi;

  constructor(private http: HttpClient) {

  }

  GetUsuarios(): Observable<Response<UsuarioListar[]>> {

    return this.http.get<Response<UsuarioListar[]>>(this.apiUrl);

  }

  DeleteUsuario(id: number | undefined): Observable<Response<UsuarioListar[]>> {

    return this.http.delete<Response<UsuarioListar[]>>(`${this.apiUrl}/${id}`);

  }

  CriarUsuario(usuario: UsuarioListar): Observable<Response<UsuarioListar[]>> {

    return this.http.post<Response<UsuarioListar[]>>(this.apiUrl, usuario);

  }

}
