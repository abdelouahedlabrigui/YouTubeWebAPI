using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YouTubeWebAPI.Models.Astronomy
{
    public class NASA_APOD_DATA {
        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("copyright")]
        public string? Copyright { get; set; }

        [JsonProperty("explanation")]
        public string? Explanation { get; set; }

        [JsonProperty("hdurl")]
        public string? HighQualityUrl { get; set; }

        [JsonProperty("media_type")]
        public string? MediaType { get; set; }

        [JsonProperty("service_version")]
        public string? ServiceVersion { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
        
    }
    public class NASA_MARS_CAMERA_INFO {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("full_name")]
        public string? FullName { get; set; }
    }
    public class NASA_MARS_ROVER_INFO {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("landing_date")]
        public DateTime? LandingDate { get; set; }

        [JsonProperty("launch_date")]
        public DateTime? LaunchDate { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("max_sol")]
        public int MaxSol { get; set; }

        [JsonProperty("max_date")]
        public DateTime? MaxDate { get; set; }

        [JsonProperty("total_photos")]
        public int TotalPhotos { get; set; }

        [JsonProperty("cameras")]
        public NASA_MARS_CAMERA_INFO[]? Cameras { get; set; }
    }
    public class NASA_MARS_CAMERA {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("rover_id")]
        public int RoverId { get; set; }

        [JsonProperty("full_name")]
        public string? FullName { get; set; }
    }
    public class NASA_MARS_PHOTO {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sol")]
        public int Sol { get; set; }

        [JsonProperty("camera")]
        public NASA_MARS_CAMERA? Camera { get; set; }

        [JsonProperty("img_src")]
        public string? ImageSource { get; set; }

        [JsonProperty("earth_date")]
        public DateTime? EarthDate { get; set; }

        [JsonProperty("rover")]
        public NASA_MARS_ROVER_INFO? RoverInfo { get; set; }
    }
    public class NASA_MARS_DATA {
        [JsonProperty("photos")]
        public NASA_MARS_PHOTO[]? Photos;
    }
    public class NASA_GENELAB_SHARDS {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("successful")]
        public int Successful { get; set; }

        [JsonProperty("skipped")]
        public int Skipped { get; set; }

        [JsonProperty("failed")]
        public int Failed { get; set; }
    }
    public class NASA_GENELAB_STUDY_PERSON {
        [JsonProperty("Last Name")]
        public string? LastName { get; set; }

        [JsonProperty("Middle Initials")]
        public string? AuthoritySourceURL { get; set; }

        [JsonProperty("First Name")]
        public string? FirstName { get; set; }
    }
    public class NASA_GENELAB_STUDY_MISSION {
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonProperty("End Date")]
        public string? EndDate { get; set; }

        [JsonProperty("Start Date")]
        public string? StartDate { get; set; }
    }
    public class NASA_GENELAB_SOURCE {
        [JsonProperty("Authoritative Source URL")]
        public string? AuthoritySourceURL { get; set; }

        [JsonProperty("Flight Program")]
        public string? FlightProgram { get; set; }

        [JsonProperty("Material Type")]
        public string? MaterialType { get; set; }

        [JsonProperty("Project Identifier")]
        public string? ProjectIdentifier { get; set; }

        [JsonProperty("Accession")]
        public string? Accession { get; set; }

        [JsonProperty("Project Link")]
        public string? ProjectLink { get; set; }

        [JsonProperty("Study Identifier")]
        public string? StudyIdentifier { get; set; }

        [JsonProperty("Study Protocol Name")]
        public string? StudyProtocolName { get; set; }

        [JsonProperty("Study Assay Technology Type")]
        public string? StudyAssayTechnologyType { get; set; }

        [JsonProperty("Acknowledgments")]
        public string? Acknowledgments { get; set; }

        [JsonProperty("Study Assay Technology Platform")]
        public string? StudyAssayTechnologyPlatform { get; set; }

        [JsonProperty("Study Person")]
        public NASA_GENELAB_STUDY_PERSON? StudyPerson { get; set; }

        [JsonProperty("Study Protocol Type")]
        public string? StudyProtocolType { get; set; }

        [JsonProperty("Study Title")]
        public string? StudyTitle { get; set; }

        [JsonProperty("Study Factor Type")]
        public string? StudyFactorType { get; set; }

        [JsonProperty("Study Public Release Date")]
        public string? StudyPublicReleaseDate { get; set; }

        [JsonProperty("Parameter Value")]
        public string? ParameterValue { get; set; }

        [JsonProperty("thumbnail")]
        public string? Thumbnail { get; set; }

        [JsonProperty("Study Factor Name")]
        public string? StudyFactorName { get; set; }

        [JsonProperty("Study Assay Measurement Type")]
        public string? StudyAssayMeasurementType { get; set; }

        [JsonProperty("Project Type")]
        public string? ProjectType { get; set; }

        [JsonProperty("Factor Value")]
        public string? FactorValue { get; set; }

        [JsonProperty("Data Source Accession")]
        public string? DataSourceAccession { get; set; }

        [JsonProperty("Data Source Type")]
        public string? DataSourceType { get; set; }

        [JsonProperty("Project Title")]
        public string? ProjectTitle { get; set; }

        [JsonProperty("Study Funding Agency")]
        public string? StudyFundingAgency { get; set; }

        [JsonProperty("Study Protocol Description")]
        public string? StudyProtocolDescription { get; set; }

        [JsonProperty("Experiment Platform")]
        public string? ExperimentPlatform { get; set; }

        [JsonProperty("Characteristics")]
        public string? Characteristics { get; set; }

        [JsonProperty("Study Grant Number")]
        public string? StudyGrantNumber { get; set; }

        [JsonProperty("Study Publication Author List")]
        public string? StudyPublicationAuthorList { get; set; }

        [JsonProperty("Mission")]
        public NASA_GENELAB_STUDY_MISSION? Mission { get; set; }

        [JsonProperty("Study Publication Title")]
        public string? StudyPublicationTitle { get; set; }

        [JsonProperty("Managing NASA Center")]
        public string? ManagingNASACenter { get; set; }

        [JsonProperty("Study Description")]
        public string? StudyDescription { get; set; }

        [JsonProperty("organism")]
        public string? Organism { get; set; }
    }
    public class NASA_GENELAB_HIGHLIGHT {
        [JsonProperty("all")]
        public string[]? All { get; set; }
    }
    public class NASA_GENELAB_HIT_INFO {
        [JsonProperty("_index")]
        public string? Index { get; set; }

        [JsonProperty("_type")]
        public string? Type { get; set; }

        [JsonProperty("_id")]
        public string? Id { get; set; }

        [JsonProperty("_score")]
        public double Score { get; set; }

        [JsonProperty("_source")]
        public NASA_GENELAB_SOURCE? Source { get; set; }

        [JsonProperty("highlight")]
        public NASA_GENELAB_HIGHLIGHT? Highlight { get; set; }
    }
    public class NASA_GENELAB_HITS {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("max_score")]
        public double MaxScore { get; set; }

        [JsonProperty("hits")]
        public NASA_GENELAB_HIT_INFO[]? Hits { get; set; }
    }
    public class NASA_GENELAB_DATA {
        [JsonProperty("took")]
        public int Took { get; set; }

        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

        [JsonProperty("_shards")]
        public NASA_GENELAB_SHARDS? Shards { get; set; }

        [JsonProperty("hits")]
        public NASA_GENELAB_HITS? Hits { get; set; }
    }
    public class NASA_EPIC_DATE_STRUCTURE {
        [JsonProperty("date")]
        public DateTime? Date { get; set; }
    }
    public class NASA_EPIC_QUATERNIONS {
        [JsonProperty("q0")]
        public double Q0 { get; set; }

        [JsonProperty("q1")]
        public double Q1 { get; set; }

        [JsonProperty("q2")]
        public double Q2 { get; set; }

        [JsonProperty("q3")]
        public double Q3 { get; set; }
    }
    public class NASA_EPIC_3D_POINT {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("z")]
        public double Z { get; set; }
    }
    public class NASA_EPIC_COORDINATES {
        [JsonProperty("centroid_coordinates")]
        public NASA_EPIC_3D_POINT? CentroIdCoordinates { get; set; }

        [JsonProperty("dscovr_j2000_position")]
        public NASA_EPIC_3D_POINT? Dscovr_J2000_Position { get; set; }

        [JsonProperty("lunar_j2000_position")]
        public NASA_EPIC_3D_POINT? Lunar_J2000_Position { get; set; }

        [JsonProperty("sun_j2000_position")]
        public NASA_EPIC_3D_POINT? Sun_J2000_Position { get; set; }

        [JsonProperty("attitude_quaternions")]
        public NASA_EPIC_QUATERNIONS? AttitudeQuaternions { get; set; }
    }
    public class NASA_EPIC_GEOGRAPHICAL_COORDINATES {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
    public class NASA_EPIC_DATA {
        [JsonProperty("identifier")]
        public string? Identifier { get; set; }

        [JsonProperty("caption")]
        public string? Caption { get; set; }

        [JsonProperty("image")]
        public string? Image { get; set; }

        [JsonProperty("version")]
        public string? Version { get; set; }

        [JsonProperty("centroid_coordinates")]
        public NASA_EPIC_GEOGRAPHICAL_COORDINATES? CentroIdCoordinates { get; set; }

        [JsonProperty("dscovr_j2000_position")]
        public NASA_EPIC_3D_POINT? Dscovr_J2000_Position { get; set; }

        [JsonProperty("lunar_j2000_position")]
        public NASA_EPIC_3D_POINT? Lunar_J2000_Position { get; set; }

        [JsonProperty("sun_j2000_position")]
        public NASA_EPIC_3D_POINT? Sun_J2000_Position { get; set; }

        [JsonProperty("attitude_quaternions")]
        public NASA_EPIC_QUATERNIONS? AttitudeQuaternions { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("coords")]
        public NASA_EPIC_COORDINATES? Coordinates { get; set; }
    }
    public class NASA_EARTH_ASSETS_RESULT {
        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }
    }
    public class NASA_EARTH_RESOURCE {
        [JsonProperty("dataset")]
        public string? Dataset { get; set; }

        [JsonProperty("planet")]
        public string? Planet { get; set; }
    }
    public class NASA_EARTH_DATA {
        [JsonProperty("cloud_score")]
        public double CloudScore { get; set; }

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("resource")]
        public NASA_EARTH_RESOURCE? Resource { get; set; }

        [JsonProperty("service_version")]
        public string? ServiceVersion { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }
    public class NASA_EARTH_ASSETS {
        [JsonProperty("count")]
        public UInt32? Count { get; set; }

        [JsonProperty("results")]
        public NASA_EARTH_ASSETS_RESULT[]? Results { get; set; }
    }
    public class NASA_ASTEROIDS_FEED_LINKS {
        [JsonProperty("next")]
        public string? Next { get; set; }

        [JsonProperty("prev")]
        public string? Previous { get; set; }

        [JsonProperty("self")]
        public string? Self { get; set; }
    }
    public class NASA_ASTEROIDS_FEED_DIAMETER_VALUES {
        [JsonProperty("estimated_diameter_min")]
        public double EstimatedDiameterMin { get; set; }

        [JsonProperty("estimated_diameter_max")]
        public double EstimatedDiameterMax { get; set; }
    }
    public class NASA_ASTEROIDS_FEED_DIAMETER {
        [JsonProperty("kilometers")]
        public NASA_ASTEROIDS_FEED_DIAMETER_VALUES? Kilometers { get; set; }

        [JsonProperty("meters")]
        public NASA_ASTEROIDS_FEED_DIAMETER_VALUES? Meters { get; set; }

        [JsonProperty("miles")]
        public NASA_ASTEROIDS_FEED_DIAMETER_VALUES? Miles { get; set; }

        [JsonProperty("feet")]
        public NASA_ASTEROIDS_FEED_DIAMETER_VALUES? Feets { get; set; }
    }
    public class NASA_ASTEROIDS_FEED_APPROACH_RELATIVE_VELOCITY {
        [JsonProperty("kilometers_per_second")]
        public string? KilometersPerSecond { get; set; }

        [JsonProperty("kilometers_per_hour")]
        public string? KilometersPerHour { get; set; }

        [JsonProperty("miles_per_hour")]
        public string? MilesPerHour { get; set; }
    }
    public class NASA_ASTEROIDS_FEED_MISS_DISTANCE {
        [JsonProperty("astronomical")]
        public string? Astronomical { get; set; }

        [JsonProperty("lunar")]
        public string? Lunar { get; set; }

        [JsonProperty("kilometers")]
        public string? Kilometers { get; set; }

        [JsonProperty("miles")]
        public string? Miles { get; set; }
    }
    public class NASA_ASTEROIDS_FEED_CLOSE_APPROACH_DATA {
        [JsonProperty("close_approach_date")]
        public DateTime? CloseApproachDate { get; set; }

        [JsonProperty("close_approach_date_full")]
        public string? CloseApproachDateFull { get; set; }

        [JsonProperty("epoch_date_close_approach")]
        public long EpochDateCloseApproach { get; set; }

        [JsonProperty("relative_velocity")]
        public NASA_ASTEROIDS_FEED_APPROACH_RELATIVE_VELOCITY? RelativeVelocity { get; set; }

        [JsonProperty("miss_distance")]
        public NASA_ASTEROIDS_FEED_MISS_DISTANCE? MissDistance { get; set; }

        [JsonProperty("orbiting_body")]
        public string? OrbitingBody { get; set; }
    }
    public class NASA_ASTEROIDS_FEED_ORBIT_CLASS {
        [JsonProperty("orbit_class_type")]
        public string? OrbitClassType { get; set; }

        [JsonProperty("orbit_class_description")]
        public string? OrbitClassDescription { get; set; }

        [JsonProperty("orbit_class_range")]
        public string? OrbitClassRange { get; set; }
    }
    public class NASA_ASTEROIDS_FEED_ORBITAL_DATA {
        [JsonProperty("orbit_id")]
        public string? OrbitId { get; set; }

        [JsonProperty("orbit_determination_date")]
        public DateTime? OrbitDeterminationDate { get; set; }

        [JsonProperty("first_observation_date")]
        public DateTime? FirstObservationDate { get; set; }

        [JsonProperty("last_observation_date")]
        public DateTime? LastObservationDate { get; set; }

        [JsonProperty("data_arc_in_days")]
        public UInt32? DataArcInDays { get; set; }

        [JsonProperty("observations_used")]
        public UInt32? ObservationsUsed { get; set; }

        [JsonProperty("orbit_uncertainty")]
        public string? OrbitUncertainty { get; set; }

        [JsonProperty("minimum_orbit_intersection")]
        public string? MinimumOrbitIntersection { get; set; }

        [JsonProperty("jupiter_tisserand_invariant")]
        public string? JupiterTisserandInvariant { get; set; }

        [JsonProperty("epoch_osculation")]
        public string? EpochOsculation { get; set; }

        [JsonProperty("eccentricity")]
        public string? Eccentricity { get; set; }

        [JsonProperty("semi_major_axis")]
        public string? SemiMajorAxis { get; set; }

        [JsonProperty("inclination")]
        public string? Inclination { get; set; }

        [JsonProperty("ascending_node_longitude")]
        public string? AscendingNodeLongitude { get; set; }

        [JsonProperty("orbital_period")]
        public string? OrbitalPeriod { get; set; }

        [JsonProperty("perihelion_distance")]
        public string? PerihelionDistance { get; set; }

        [JsonProperty("perihelion_argument")]
        public string? PerihelionArgument { get; set; }

        [JsonProperty("aphelion_distance")]
        public string? AphelionDistance { get; set; }

        [JsonProperty("perihelion_time")]
        public string? PerihelionTime { get; set; }

        [JsonProperty("mean_anomaly")]
        public string? MeanAnomaly { get; set; }

        [JsonProperty("mean_motion")]
        public string? MeanMotion { get; set; }

        [JsonProperty("equinox")]
        public string? Equinox { get; set; }

        [JsonProperty("orbit_class")]
        public NASA_ASTEROIDS_FEED_ORBIT_CLASS? OrbitClass { get; set; }
    }
    public class NASA_ASTEROIDS_FEED_NEO {
        [JsonProperty("links")]
        public NASA_ASTEROIDS_FEED_LINKS? Links { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("neo_reference_id")]
        public string? NeoReferenceId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("nasa_jpl_url")]
        public string? NasaJplUrl { get; set; }

        [JsonProperty("absolute_magnitude_h")]
        public double AbsoluteMagnitudeH { get; set; }

        [JsonProperty("estimated_diameter")]
        public NASA_ASTEROIDS_FEED_DIAMETER? EstimatedDiameter { get; set; }

        [JsonProperty("is_potentially_hazardous_asteroid")]
        public bool IsPotentiallyHazardousAsteroid { get; set; }

        [JsonProperty("close_approach_data")]
        public NASA_ASTEROIDS_FEED_CLOSE_APPROACH_DATA[]? CloseApproachData { get; set; }

        [JsonProperty("orbital_data")]
        public NASA_ASTEROIDS_FEED_ORBITAL_DATA? OrbitalData { get; set; }

        [JsonProperty("is_sentry_object")]
        public bool IsSentryObject { get; set; }
    }
    public class NASA_ASTEROIDS_FEED_DATA {
        [JsonProperty("links")]
        public NASA_ASTEROIDS_FEED_LINKS? Links { get; set; }

        [JsonProperty("element_count")]
        public UInt32? ElementCount { get; set; }

        [JsonProperty("near_earth_objects")]
        public Dictionary<string, List<NASA_ASTEROIDS_FEED_NEO>>? NearEarthObjects { get; set; }
    }
    public class NASA_ASTEROIDS_BROWSE_PAGE {
        [JsonProperty("size")]
        public UInt32? Size { get; set; }

        [JsonProperty("total_elements")]
        public UInt32? TotalElements { get; set; }

        [JsonProperty("total_pages")]
        public UInt32? TotalPages { get; set; }

        [JsonProperty("number")]
        public UInt32? Number { get; set; }
    }
    public class NASA_ASTEROIDS_BROWSE_DATA {
        [JsonProperty("links")]
        public NASA_ASTEROIDS_FEED_LINKS? Links { get; set; }
        
        [JsonProperty("page")]
        public NASA_ASTEROIDS_BROWSE_PAGE? Page { get; set; }

        [JsonProperty("near_earth_objects")]
        public NASA_ASTEROIDS_FEED_NEO[]? NearEarthObjects { get; set; }
    }
    public class NASA_ASTEROIDS_SENTRY_LINKS {
        [JsonProperty("near_earth_object_parent")]
        public string? NearEarthObjectParent { get; set; }

        [JsonProperty("self")]
        public string? Self { get; set; }
    }
    public class NASA_ASTEROIDS_SENTRY_OBJECT {
        [JsonProperty("links")]
        public NASA_ASTEROIDS_SENTRY_LINKS? Links { get; set; }

        [JsonProperty("spkId")]
        public string? SpkId { get; set; }

        [JsonProperty("designation")]
        public string? Designation { get; set; }

        [JsonProperty("sentryId")]
        public string? SentryId { get; set; }

        [JsonProperty("fullname")]
        public string? Fullname { get; set; }

        [JsonProperty("year_range_min")]
        public string? YearRangeMin { get; set; }

        [JsonProperty("year_range_max")]
        public string? YearRangeMax { get; set; }

        [JsonProperty("potential_impacts")]
        public long PotentialImpacts { get; set; }

        [JsonProperty("impact_probability")]
        public double ImpactProbability { get; set; }

        [JsonProperty("v_infinity")]
        public double V_Infinity { get; set; }

        [JsonProperty("absolute_magnitude")]
        public double AbsoluteMagnitude { get; set; }

        [JsonProperty("estimated_diameter")]
        public double EstimatedDiameter { get; set; }

        [JsonProperty("palermo_scale_ave")]
        public double PalermoScaleAve { get; set; }

        [JsonProperty("Palermo_scale_max")]
        public double PalermoScaleMax { get; set; }

        [JsonProperty("torino_scale")]
        public string? TorinoScale { get; set; }

        [JsonProperty("last_obs")]
        public string? LastObs { get; set; }

        [JsonProperty("last_obs_jd")]
        public string? LastObs_jd { get; set; }

        [JsonProperty("url_nasa_details")]
        public string? UrlNasaDetails { get; set; }

        [JsonProperty("url_orbital_elements")]
        public string? UrlOrbitalElements { get; set; }

        [JsonProperty("is_active_sentry_object")]
        public bool IsActiveSentryObject { get; set; }

        [JsonProperty("average_lunar_distance")]
        public double AverageLunarDistance { get; set; }
    }
    public class NASA_ASTEROIDS_SENTRY_DATA {
        [JsonProperty("links")]
        public NASA_ASTEROIDS_FEED_LINKS? Links { get; set; }

        [JsonProperty("page")]
        public NASA_ASTEROIDS_BROWSE_PAGE? Page { get; set; }

        [JsonProperty("sentry_objects")]
        public NASA_ASTEROIDS_SENTRY_OBJECT[]? SentryObjects { get; set; }
    }
    public class NASA_ASTEROIDS_NEO_STATS {
        [JsonProperty("near_earth_object_count")]
        public long NeoCount { get; set; }

        [JsonProperty("close_approach_count")]
        public long CloseApproachCount { get; set; }

        [JsonProperty("last_updated")]
        public DateTime? LastUpdated { get; set; }

        [JsonProperty("source")]
        public string? Source { get; set; }

        [JsonProperty("nasa_jpl_url")]
        public string? NasaJplUrl { get; set; }
    }
}