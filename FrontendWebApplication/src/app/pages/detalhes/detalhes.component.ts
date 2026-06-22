import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Funcionario } from '../../Models/Funcionarios';
import { FuncionarioService } from '../../services/funcionario.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-detalhes',
  templateUrl: './detalhes.component.html',
  styleUrl: './detalhes.component.css'
})
export class DetalhesComponent implements OnInit {
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

    this.carregarFuncionario(id);
  }

  carregarFuncionario(id: number) {
    this.loading = true;
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

  alternarStatus() {
    if (!this.funcionario || !this.funcionario.id) return;
    
    const isAtivo = this.funcionario.status;
    const request = isAtivo 
      ? this.funcionarioService.inativarFuncionario(this.funcionario.id)
      : this.funcionarioService.ativarFuncionario(this.funcionario.id);

    request.subscribe({
      next: (response) => {
        if (response.status) {
          Swal.fire({
            title: isAtivo ? 'Inativado!' : 'Ativado!',
            text: `Funcionário ${isAtivo ? 'inativado' : 'ativado'} com sucesso!`,
            icon: 'success',
            background: '#1e293b',
            color: '#f8fafc',
            confirmButtonColor: '#6366f1'
          });
          // Reload details
          this.carregarFuncionario(this.funcionario!.id!);
        } else {
          Swal.fire({
            title: 'Erro',
            text: response.mensagem || 'Erro ao alterar a situação do funcionário.',
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

  excluir() {
    if (!this.funcionario || !this.funcionario.id) return;
    
    Swal.fire({
      title: 'Excluir Funcionário',
      text: `Tem certeza de que deseja excluir o funcionário ${this.funcionario.nome} ${this.funcionario.sobrenome}?`,
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
        this.funcionarioService.excluirFuncionario(this.funcionario!.id!).subscribe({
          next: (response) => {
            if (response.status) {
              Swal.fire({
                title: 'Excluído!',
                text: 'Funcionário excluído com sucesso!',
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
                text: response.mensagem || 'Erro ao excluir o funcionário.',
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
