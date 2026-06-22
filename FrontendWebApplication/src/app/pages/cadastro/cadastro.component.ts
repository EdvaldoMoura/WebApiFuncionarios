import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Funcionario } from '../../Models/Funcionarios';
import { FuncionarioService } from '../../services/funcionario.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.css'
})
export class CadastroComponent {
  
  constructor(
    private funcionarioService: FuncionarioService,
    private router: Router
  ) {}

  criarFuncionario(funcionario: Funcionario) {
    this.funcionarioService.cadastrarFuncionario(funcionario).subscribe({
      next: (response) => {
        if (response.status) {
          Swal.fire({
            title: 'Cadastrado!',
            text: 'Funcionário cadastrado com sucesso!',
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
            text: response.mensagem || 'Ocorreu um erro ao cadastrar o funcionário.',
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
