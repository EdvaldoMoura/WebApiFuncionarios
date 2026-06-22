import { Component, OnInit } from '@angular/core';
import { UsuariosService } from '../../services/usuarios.service';
import { UsuarioListar } from '../../Models/Usuarios';
import Swal from 'sweetalert2';
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  constructor(private usuariosService: UsuariosService) { }

  usuarios: UsuarioListar[] = [];
  usuariosGeral: UsuarioListar[] = [];

  ngOnInit(): void {
    this.usuariosService.GetUsuarios().subscribe((response) => {
      this.usuarios = response.dados;
      this.usuariosGeral = response.dados;
      console.log(response);
    });
  }

  DeletarUsuario(id: number | undefined) {

    Swal.fire({
      title: "Deletar Usuário?",
      text: "Tem certeza que deseja deletar este usuário?",
      html: `
        <p style="color: red; font-weight: bold;">Esta ação é irreversível!'</p>`,
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Sim, deletar!",
      cancelButtonText: "Não, cancelar!",
    }).then((result) => {
      if (result.isConfirmed) {

        this.usuariosService.DeleteUsuario(id).subscribe((response) => {
          this.usuarios = response.dados;
          this.usuariosGeral = response.dados;
          console.log(response);
          window.location.reload();
        });

        Swal.fire({
          title: "Deletado!",
          text: "Usuário deletado com sucesso!",
          icon: "success"
        });

      }

    });


  }

  PesquisarUsuario(event: Event) {
    const valor = (event.target as HTMLInputElement).value.trim().toLowerCase();
    this.usuarios = this.usuariosGeral.filter((usuario) => usuario.nome.toLowerCase().includes(valor));
  }

}
