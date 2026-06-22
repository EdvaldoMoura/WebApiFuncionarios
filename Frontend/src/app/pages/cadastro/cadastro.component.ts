import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormularioComponent } from "../../componente/formulario/formulario.component";
import { UsuarioListar } from '../../Models/Usuarios';
import { UsuariosService } from '../../services/usuarios.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [FormularioComponent],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.css'
})
export class CadastroComponent {

  constructor(private usuarioService: UsuariosService, private router: Router) { }

  criarUsuario(usuario: UsuarioListar) {

    // Converte situacao de string para boolean (o <select> envia "true"/"false" como string)
    usuario.situacao = usuario.situacao.toString() === 'true';

    this.usuarioService.CriarUsuario(usuario).subscribe({
      next: (response) => {
        console.log(response);
        Swal.fire({
          title: "Sucesso!",
          text: "Usuário cadastrado com sucesso!",
          icon: "success"
        }).then(() => {
          this.router.navigate(['/']);
        });
      },
      error: (err) => {
        console.error(err);
        Swal.fire({
          title: "Erro!",
          text: "Erro ao cadastrar usuário. Tente novamente.",
          icon: "error"
        });
      }
    });
  }

}
