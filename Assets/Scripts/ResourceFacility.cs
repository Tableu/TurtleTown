public class ResourceFacility : Facility
{
    private int _value;
    private Resource _resource;
    
    private new void Start()
    {
        base.Start();
        _value = _initData.Value;
        _resource = _initData.Resource;
    }

    protected override void TaskStart()
    {
        
    }

    protected override void TaskEnd()
    {
        
    }

    protected override void TaskTick()
    {
        int count = Workers?.Count ?? 1;
        _resource.Value += _value * count;
    }
}