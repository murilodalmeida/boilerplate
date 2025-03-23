namespace FwksLabs.Libs.Core.Contracts.Requests;

public record PageRequest(int PageNumber, int PageSize)
{
    public int GetSkip() => PageSize < 0 ? -1 : (PageNumber - 1) * PageSize;
}