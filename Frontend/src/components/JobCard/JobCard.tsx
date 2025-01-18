import "./JobCard.css";

const JobCard = (props: {position: string, company: string, techStack: string[]}) => {
    return (
        <>
            <div className="card border-primary my-3">
                <div className="card-header">{props.company}</div>
                <div className="card-body align-items-baseline">
                    <h4 className="card-title">{props.position}</h4>
                    <ul className="card-text p-0">
                        {props.techStack.map((item, index) => (
                        <li key={index} className="list-inline-item">| {item} |</li>
                        ))}
                    </ul>
                </div>
            </div>
        </>
    );
};

export default JobCard;