import { Component, EventEmitter, OnInit, Output, output } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UsuarioListar } from '../../Models/Usuarios';

@Component({
  selector: 'app-formulario',
  standalone: true,
  imports: [RouterModule, FormsModule, ReactiveFormsModule],
  templateUrl: './formulario.component.html',
  styleUrl: './formulario.component.css'
})
export class FormularioComponent implements OnInit {


  @Output() onSubmit = new EventEmitter<UsuarioListar>();

  usuarioForm!: FormGroup;

  ngOnInit(): void {

    this.usuarioForm = new FormGroup({

      id: new FormControl(0),
      nome: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      cargo: new FormControl('', Validators.required),
      salario: new FormControl('', Validators.required),
      cpf: new FormControl('', Validators.required),
      situacao: new FormControl('', Validators.required),
      senha: new FormControl('', Validators.required)

    })
  }

  submit() {
    //console.log(this.usuarioForm.value);
    this.onSubmit.emit(this.usuarioForm.value);
  }
}
