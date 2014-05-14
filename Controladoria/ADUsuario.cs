using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;

namespace ProtocoloWeb
{
    public class ADUsuario
    {
        private String _PrimeiroNome;
        public String PrimeiroNome
        {
            get { return _PrimeiroNome; }
        }

        private String _NomeDoMeio;
        public String NomeDoMeio
        {
            get { return _NomeDoMeio; }
        }

        private String _UltimoNome;
        public String UltimoNome
        {
            get { return _UltimoNome; }
        }
        
        private String _NomeCompleto;
        public String NomeCompleto
        {
            get { return _NomeCompleto; }
        }

        private String _Login;
        public String Login
        {
            get { return _Login; }
        }

        private String _Cep;
        public String Cep
        {
            get { return _Cep; }
        }
        
        private String _Endereco;
        public String Endereco
        {
            get { return _Endereco; }
        }
        
        private String _Cidade;
        public String Cidade
        {
            get { return _Cidade; }
        }

        private String _Estado;
        public String Estado
        {
            get { return _Estado; }
        }
        
        private String _Pais;
        public String Pais
        {
            get { return _Pais; }
        }

        private String _email;
        public String Email
        {
            get { return _email; }
        }

        private String _celular;
        public String Celular
        {
            get { return _celular; }
        }
        
        private String _telefone;
        public String Telefone
        {
            get { return _telefone; }
        }
        
        private String _telefoneComercial;
        public String TelefoneComercial
        {
            get { return _telefoneComercial; }
        }

        private String _titutlo;
        public String Titulo
        {
            get { return _titutlo; }
        }
        
        private String _dataCriacao;
        public String DataCriacao
        {
            get { return _dataCriacao; }
        }
        
        private String _dataUltimaModificacao;
        public String DataUltimaModificacao
        {
            get { return _dataUltimaModificacao; }
        }

        private String _departamento;
        public String Departamento
        {
            get { return _departamento; }
        }
        
        private String _compania;
        public String Compania
        {
            get { return _compania; }
        }

        private ADUsuario(DirectoryEntry directoryUser)
        {
 
            _PrimeiroNome = GetProperty(directoryUser, ADProp.PRIMEIRONOME);
            _NomeDoMeio = GetProperty(directoryUser, ADProp.NOMEDOMEIO);
            _UltimoNome = GetProperty(directoryUser, ADProp.ULTIMONOME);
            _NomeCompleto = GetProperty(directoryUser, ADProp.NOMECOMPLETO);
            _Login = GetProperty(directoryUser, ADProp.LOGIN);

            _Cep = GetProperty(directoryUser, ADProp.CEP);
            _Endereco = GetProperty(directoryUser, ADProp.ENDERECO);
            _Cidade = GetProperty(directoryUser, ADProp.CIDADE);
            _Estado = GetProperty(directoryUser, ADProp.ESTADO);
            _Pais = GetProperty(directoryUser, ADProp.PAIS);

            _email = GetProperty(directoryUser, ADProp.EMAIL);
            _telefone = GetProperty(directoryUser, ADProp.TELEFONE);
            _celular = GetProperty(directoryUser, ADProp.CELULAR);
            _telefoneComercial = GetProperty(directoryUser, ADProp.TELEFONECOMERCIAL);

            _titutlo = GetProperty(directoryUser, ADProp.TITULO);

            _dataCriacao = GetProperty(directoryUser, ADProp.DATADACRIACAO);
            _dataUltimaModificacao = GetProperty(directoryUser, ADProp.DATAULTIMAMODIFICACAO);

            _departamento= GetProperty(directoryUser, ADProp.DEPARTAMENTO);
            _compania = GetProperty(directoryUser, ADProp.COMPANIA);
            
        }
 
        private static String GetProperty(DirectoryEntry userDetail, String propertyName)
        {
            if (userDetail.Properties.Contains(propertyName))
            {
                return userDetail.Properties[propertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static ADUsuario GetUser(DirectoryEntry directoryUser)
        {
            return new ADUsuario(directoryUser);
        }
    }
}
