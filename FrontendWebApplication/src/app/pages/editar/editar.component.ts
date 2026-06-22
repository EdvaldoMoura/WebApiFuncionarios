import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Funcionario } from '../../Models/Funcionarios';
import { FuncionarioService } from '../../services/funcionario.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html',
  styleUrl: './editar.component.css'
})
export class EditarComponent implements OnInit {
  funcionario: Funcionario | null = null;
  loading: boolean = true;

  constructor(
    private funcionarioService: FuncionarioService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) {
      Swal.fire({
        title: 'ID Inválido',
        text: 'O identificador do funcionário é inválido.',
        icon: 'warning',
        background: '#1e293b',
        color: '#f8fafc',
        confirmButtonColor: '#6366f1'
      }).then(() => {
        this.router.navigate(['/']);
      });
      return;
    }

    this.funcionarioService.obterFuncionarioPorId(id).subscribe({
      next: (response) => {
        if (response.status) {
          this.funcionario = response.dados;
        } else {
          Swal.fire({
            title: 'Não Encontrado',
            text: response.mensagem || 'Funcionário não encontrado.',
            icon: 'warning',
            background: '#1e293b',
            color: '#f8fafc',
            confirmButtonColor: '#6366f1'
          }).then(() => {
            this.router.navigate(['/']);
          });
        }
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        Swal.fire({
          title: 'Erro de Conexão',
          text: 'Erro ao se conectar ao servidor.',
          icon: 'error',
          background: '#1e293b',
          color: '#f8fafc',
          confirmButtonColor: '#6366f1'
        }).then(() => {
          this.router.navigate(['/']);
        });
        this.loading = false;
      }
    });
  }

  editarFuncionario(funcionario: Funcionario) {
    this.funcionarioService.editarFuncionario(funcionario).subscribe({
      next: (response) => {
        if (response.status) {
          Swal.fire({
            title: 'Atualizado!',
            text: 'Funcionário atualizado com sucesso!',
            icon: 'success',
            background: '#1e293b',
            color: '#f8fafc',
            confirmButtonColor: '#6366f1'
          }).then(() => {
            this.router.navigate(['/']);
          });
        } else {
          Swal.fire({
            title: 'Erro',
            text: response.mensagem || 'Ocorreu um erro ao atualizar o funcionário.',
            icon: 'error',
            background: '#1e293b',
            color: '#f8fafc',
            confirmButtonColor: '#6366f1'
          });
        }
      },
      error: (err) => {
        console.error(err);
        Swal.fire({
          title: 'Erro de Conexão',
          text: 'Erro ao se conectar ao servidor.',
          icon: 'error',
          background: '#1e293b',
          color: '#f8fafc',
          confirmButtonColor: '#6366f1'
        });
      }
    });
  }
}
