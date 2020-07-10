import { Component, OnInit, TemplateRef } from '@angular/core';
import { ProdutoService } from '../_services/produto.service';
import { Produto } from '../_models/Produto';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { HttpEventType, HttpEvent } from '@angular/common/http';


@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.css']
})
export class ProdutosComponent implements OnInit {

  titulo = 'Produtos';

  produtosFiltrados: Produto [];
  produtos: Produto [];
  produto: Produto;
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  tituloModal = '';
  _filtroLista= '';
  registerForm: FormGroup;
  bodyDeletarProduto = '';
  fileNameImagem: any;
  progresso = 0;
  file: File;
  http: any;

  constructor(
    private produtoService: ProdutoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private toastr: ToastrService
    ) { }


    get filtroLista(): string {
      return this._filtroLista;
    }
    set filtroLista(value: string) {
      this._filtroLista = value;
      this.produtosFiltrados = this.filtroLista ? this.filtrarProdutos(this.filtroLista) : this.produtos;
    }

    editarProduto(produto: Produto, template: any) {
      this.tituloModal = 'Editar';
      this.openModal(template);
      this.produto = produto;
      this.registerForm.patchValue(produto);
    }

    novoProduto(template: any) {
      this.tituloModal = 'Adicionar';
      this.openModal(template);
    }

    excluirProduto(produto: Produto, template: any) {
      this.openModal(template);
      this.produto = produto;
      this.bodyDeletarProduto = `Tem certeza que deseja excluir o Produto: ${produto.nome}, Código: ${produto.id}`;
    }

    confirmeDelete(template: any) {
      template.hide();
      this.produtoService.deleteProduto(this.produto.id).subscribe(
        () => {
            this.getProdutos();
            this.toastr.success('Deletado com sucesso');
          }, error => {
            this.toastr.error('Erro ao tentar deletar');
            console.log(error);
          }
      );
    }

    openModal(template: any) {
      this.progresso = 0;
      this.registerForm.reset();
      template.show();
    }

    ngOnInit() {
      this.validation();
      this.getProdutos();
    }

  filtrarProdutos(filtrarPor: string): Produto[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.produtos.filter(
      produto => produto.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  onFileChange(produt) {
      this.file = produt.target.files;
  }

  uploadImagem(template) {

    const nomeArquivo = this.produto.imagemUrl.split('\\', 3);
    this.produto.imagemUrl = nomeArquivo[2];
    this.produtoService.postUpload(this.file, nomeArquivo[2])
    .subscribe( (event: HttpEvent<Object>) => {
      if (event.type === HttpEventType.Response){
        template.hide();
        this.toastr.success(`${this.tituloModal === 'Editar' ? 'Editado' : 'Adicionado' } com sucesso`);
      } else if (event.type === HttpEventType.UploadProgress) {
        const percentDone = Math.round((event.loaded * 100) / event.total);
        this.progresso = percentDone;
      }
    });
  }

  salvarAlteracao(template: any) {
      if (this.tituloModal === 'Adicionar') {
        this.produto = Object.assign({}, this.registerForm.value);
        this.uploadImagem(template);
        this.produtoService.postProduto(this.produto).subscribe(
          (novoProduto: Produto) => {
            console.log(novoProduto);
            this.getProdutos();
          }, error => {
            this.toastr.error('Erro ao Inserir');
            /*console.log(error);*/
          }
        );
      } else {
        this.produto = Object.assign({id: this.produto.id}, this.registerForm.value);
        if (this.registerForm.valid){
          this.uploadImagem(template);
          }
        this.produtoService.putProduto(this.produto).subscribe(
           () => {

            this.getProdutos();
          }, error => {
            this.toastr.error(`Erro ao Inserir: ${error}`);
            /*console.log(error);*/
          }
        );
      }
  }

  validation() {
    this.registerForm = this.fb.group({
      nome: ['',
      [Validators.required, Validators.maxLength(100)]],
      preco: ['',
      [Validators.required, Validators.maxLength(10)]],
      descricao: ['', Validators.maxLength(100)],
      status: ['', Validators.required],
      imagemUrl: ['', Validators.required],
    });
  }

  getProdutos() {
     this.produtoService.getAllProduto().subscribe(
       (_produtos: Produto[]) => {
         this.produtos = _produtos;
         this.produtosFiltrados = this.produtos;
      }, error => {
         /*console.log(error);*/
         this.toastr.error(`Erro ao Inserir: ${error}`);
       });
  }

}
