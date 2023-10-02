using Data.DTOs;

namespace IntegrationProjectFrontend.ViewModels
{
    public class UsersViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Dni { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

        public bool IsDeleted { get; set; }


        public static implicit operator UsersViewModel(UserDTO usuario)
        {
            var usuariosViewModel = new UsersViewModel();
            usuariosViewModel.Id = usuario.Id;
            usuariosViewModel.FirstName = usuario.FirstName;
            usuariosViewModel.LastName = usuario.LastName;
            usuariosViewModel.Email = usuario.Email;
            usuariosViewModel.Password = usuario.Password;
            usuariosViewModel.RoleId = usuario.RoleId;
            return usuariosViewModel;
        }
    }
}
