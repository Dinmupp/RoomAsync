namespace Domain.Allergy
{
    public abstract class Allergy : IEquatable<Allergy>
    {
        public abstract string Name { get; }

        public override string ToString() => Name;

        public override bool Equals(object? obj) => obj is Allergy other && Name == other.Name;
        public bool Equals(Allergy? other) => other is not null && Name == other.Name;
        public override int GetHashCode() => Name.GetHashCode();

        // Factory for known allergies
        public static Allergy FromName(string name) => name.ToLowerInvariant() switch
        {
            "jordnöt" or "peanut" => new PeanutAllergy(),
            "skaldjur" or "shellfish" => new ShellfishAllergy(),
            "mjölk" or "milk" => new MilkAllergy(),
            "ägg" or "egg" => new EggAllergy(),
            "soja" or "soya" => new SoyAllergy(),
            "gluten" => new GlutenAllergy(),
            "fisk" or "fish" => new FishAllergy(),
            "selleri" or "celery" => new CeleryAllergy(),
            "senap" or "mustard" => new MustardAllergy(),
            "sesam" or "sesame" => new SesameAllergy(),
            "lupin" or "lupine" => new LupinAllergy(),
            "sulfit" or "sulphite" or "sulfite" => new SulfiteAllergy(),
            _ => new CustomAllergy(name)
        };
    }

    public sealed class PeanutAllergy : Allergy { public override string Name => "Jordnöt"; }
    public sealed class ShellfishAllergy : Allergy { public override string Name => "Skaldjur"; }
    public sealed class MilkAllergy : Allergy { public override string Name => "Mjölk"; }
    public sealed class EggAllergy : Allergy { public override string Name => "Ägg"; }
    public sealed class SoyAllergy : Allergy { public override string Name => "Soja"; }
    public sealed class GlutenAllergy : Allergy { public override string Name => "Gluten"; }
    public sealed class FishAllergy : Allergy { public override string Name => "Fisk"; }
    public sealed class CeleryAllergy : Allergy { public override string Name => "Selleri"; }
    public sealed class MustardAllergy : Allergy { public override string Name => "Senap"; }
    public sealed class SesameAllergy : Allergy { public override string Name => "Sesam"; }
    public sealed class LupinAllergy : Allergy { public override string Name => "Lupin"; }
    public sealed class SulfiteAllergy : Allergy { public override string Name => "Sulfit"; }

    public sealed class CustomAllergy : Allergy
    {
        private readonly string _name;
        public CustomAllergy(string name) => _name = name;
        public override string Name => _name;
    }
}
