using MaxiTransfers.Api.Entidades.DTO.Beneficiario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaxiTransfers.Api.Servicios
{
    public interface IBeneficiariosService
    {
        public Task<string> CrearBeneficiario(BeneficiariosRequestCreate obj);
        public Task<List<BeneficiariosResponse>> ListarBeneficiarios();
        public Task<BeneficiariosResponse> ObtenerBeneficiarios(int objbeneficiario);
        public Task<bool> ActualizarBeneficiario(BeneficiariosRequestUpdate obj);
        public Task<bool> EliminarBeneficiario(int Beneficiario);
    }
}
