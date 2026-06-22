import { Component, OnInit } from '@angular/core';
import { FuncionarioService } from '../../services/funcionario.service';
import { Funcionario } from '../../Models/Funcionarios';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  funcionarios: Funcionario[] = [];
  funcionariosGeral: Funcionario[] = [];

  constructor(private funcionarioService: FuncionarioService) {}

  ngOnInit(): void {
    this.funcionarioService.listarFuncionarios().subscribe({
      next: (response) => {
        if (response.status && response.dados) {
          this.funcionarios = response.dados;
          this.funcionariosGeral = response.dados;
        }
      },
      error: (err) => {
        console.error('Erro ao listar funcionários:', err);
      }
    });
  }

  search(event: Event) {
    const target = event.target as HTMLInputElement;
    const value = target.value.toLowerCase().trim();

    if (!value) {
      this.funcionarios = this.funcionariosGeral;
      return;
    }

    this.funcionarios = this.funcionariosGeral.filter((funcionario) => {
      return funcionario.nome.toLowerCase().includes(value) || 
             funcionario.sobrenome.toLowerCase().includes(value);
    });
  }

  excluirFuncionario(id: number) {
    Swal.fire({
      title: 'Excluir Funcionário',
      text: 'Tem certeza de que deseja excluir este funcionário?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#ef4444',
      cancelButtonColor: '#64748b',
      confirmButtonText: 'Sim, excluir!',
      cancelButtonText: 'Cancelar',
      background: '#1e293b',
      color: '#f8fafc'
    }).then((result) => {
      if (result.isConfirmed) {
        this.funcionarioService.excluirFuncionario(id).subscribe({
          next: (response) => {
            if (response.status) {
              Swal.fire({
                title: 'Excluído!',
                text: 'Funcionário excluído com sucesso!',
                icon: 'success',
                background: '#1e293b',
                color: '#f8fafc',
                confirmButtonColor: '#6366f1'
              });
              // Update UI list directly without reloading the page
              this.funcionarios = this.funcionarios.filter(f => f.id !== id);
              this.funcionariosGeral = this.funcionariosGeral.filter(f => f.id !== id);
            } else {
              Swal.fire({
                title: 'Erro',
                text: response.mensagem,
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
              text: 'Erro ao excluir o funcionário.',
              icon: 'error',
              background: '#1e293b',
              color: '#f8fafc',
              confirmButtonColor: '#6366f1'
            });
          }
        });
      }
    });
  }

  getDepartamento(value: string | undefined): string {
    if (value === undefined || value === null) return '';
    const depts: Record<string, string> = {
      '0': 'RH',
      '1': 'Financeiro',
      '2': 'Compras',
      '3': 'Atendimento',
      '4': 'Zeladoria',
      '5': 'Tecnologia'
    };
    return depts[value.toString()] || 'Outros';
  }

  getTurno(value: string | undefined): string {
    if (value === undefined || value === null) return '';
    const turnos: Record<string, string> = {
      '0': 'Manhã',
      '1': 'Tarde',
      '2': 'Noite'
    };
    return turnos[value.toString()] || 'Outros';
  }
}
