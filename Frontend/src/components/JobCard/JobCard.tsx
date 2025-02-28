import "./JobCard.css";

const JobCard = (props: { title?: string, companyName?: string, requiredSkills?: string[], key?: unknown }) => {
    return (
        <>
            <div className="card border-primary my-3">
                <div className="card-header">{props?.companyName}</div>
                <div className="card-body align-items-baseline">
                    <h4 className="card-title">{props?.title}</h4>
                    <ul className="card-text p-0">
                        {props?.requiredSkills?.map((item, index) => (
                        <li key={index} className="list-inline-item">| {item} |</li>
                        ))}
                    </ul>
                </div>
            </div>
        </>
    );
};

export default JobCard;