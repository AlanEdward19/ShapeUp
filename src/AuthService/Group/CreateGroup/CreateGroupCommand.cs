namespace AuthService.Group.CreateGroup;

public class CreateGroupCommand
{
    /// <summary>
    /// Nome do grupo
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Descrição do grupo
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Método para converter o comando em uma entidade Group
    /// </summary>
    /// <returns></returns>
    public Group ToEntity()
    {
        return new Group(Name, Description);
    }
}