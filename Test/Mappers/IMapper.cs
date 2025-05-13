namespace Test.Mappers;

public interface IMapper<TE, TR>
{
    TR MapEntityToResponse(TE entity);
}