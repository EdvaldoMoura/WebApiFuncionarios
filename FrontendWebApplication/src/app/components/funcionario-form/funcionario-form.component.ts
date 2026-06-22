import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Funcionario } from '../../Models/Funcionarios';

@Component({
  selector: 'app-funcionario-form',
  templateUrl: './funcionario-form.component.html',
  styleUrl: './funcionario-form.component.css'
})
export class FuncionarioFormComponent implements OnInit {
  @Input() btnAcao!: string;
  @Input() btnTitulo!: string;
  @Input() funcionarioDados: Funcionario | null = null;
  @Output() onSubmit = new EventEmitter<Funcionario>();

  funcionarioForm!: FormGroup;

  ngOnInit(): void {
    this.funcionarioForm = new FormGroup({
      id: new FormControl(this.funcionarioDados ? this.funcionarioDados.id : 0),
      nome: new FormControl(this.funcionarioDados ? this.funcionarioDados.nome : '', [Validators.required]),
      sobrenome: new FormControl(this.funcionarioDados ? this.funcionarioDados.sobrenome : '', [Validators.required]),
      departamento: new FormControl(this.funcionarioDados ? this.funcionarioDados.departamento.toString() : '0', [Validators.required]),
      turno: new FormControl(this.funcionarioDados ? this.funcionarioDados.turno.toString() : '0', [Validators.required]),
      status: new FormControl(this.funcionarioDados ? this.funcionarioDados.status : true),
      dataCriacao: new FormControl(this.funcionarioDados ? this.funcionarioDados.dataCriacao : ''),
      dataAtualizacao: new FormControl(this.funcionarioDados ? this.funcionarioDados.dataAtualizacao : '')
    });
  }

  submit() {
    if (this.funcionarioForm.invalid) {
      return;
    }

    const rawFormValue = this.funcionarioForm.value;
    
    // Construct Funcionario object with correct types for backend enum expectations
    // HTML <select> returns strings ("0","5"), but C# System.Text.Json expects integers for enums
    const funcionario: any = {
      nome: rawFormValue.nome,
      sobrenome: rawFormValue.sobrenome,
      departamento: Number(rawFormValue.departamento),
      turno: Number(rawFormValue.turno),
      status: !!rawFormValue.status
    };

    // Include id only when editing (id > 0)
    if (rawFormValue.id) {
      funcionario.id = rawFormValue.id;
    }

    this.onSubmit.emit(funcionario);
  }
}
